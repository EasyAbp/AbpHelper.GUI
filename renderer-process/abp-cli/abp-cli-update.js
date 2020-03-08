const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let isIncludePreviews = false
let isOnlyNpm = false
let isOnlyNuget = false

let consoleNode = document.getElementById('box-abp-cli-update').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('update-execute')
const selectFileBtn = document.getElementById('update-select-folder-btn')
const includePreviewsCheckbox = document.getElementById('update-include-previews')
const onlyNpmCheckbox = document.getElementById('update-only-npm')
const onlyNugetCheckbox = document.getElementById('update-only-nuget')

selectFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('update-folder').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

execBtn.addEventListener('click', (event) => {
  runExec()
})

includePreviewsCheckbox.addEventListener('click', (event) => {
  isIncludePreviews = includePreviewsCheckbox.checked
})

onlyNpmCheckbox.addEventListener('click', (event) => {
  isOnlyNpm = onlyNpmCheckbox.checked
})

onlyNugetCheckbox.addEventListener('click', (event) => {
  isOnlyNuget = onlyNugetCheckbox.checked
})

function runExec() {
  let cmdPath = document.getElementById('update-folder').value
  if (isRunning || !cmdPath) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('update-process').style.display = 'block'

  let cmdStr = 'abp update'
  if (isIncludePreviews) cmdStr += ' -p'
  if (isOnlyNpm) cmdStr += ' --npm'
  if (isOnlyNuget) cmdStr += ' --nuget'
  clearConsoleContent()
  addConsoleContent(cmdStr + '\n\nRunning...\n')
  scrollConsoleToBottom()
  console.log(cmdStr)
  workerProcess = exec('chcp 65001 & ' + cmdStr, {cwd: cmdPath})
  
  workerProcess.stdout.on('data', function (data) {
    addConsoleContent(data)
    scrollConsoleToBottom()
  });
 
  workerProcess.stderr.on('data', function (data) {
    addConsoleContent(data)
    scrollConsoleToBottom()
  });
 
  workerProcess.on('close', function (code) {
    isRunning = false
    execBtn.disabled = false
  })

  function scrollConsoleToBottom() {
    consoleNode.scrollTo(0, consoleNode.scrollHeight)
  }

  function addConsoleContent(text) {
    consoleNode.appendChild(document.createTextNode(text))
  }

  function clearConsoleContent() {
    consoleNode.innerHTML = ''
  }
}