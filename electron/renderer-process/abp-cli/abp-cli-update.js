const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let isIncludePreviews = false
let isOnlyNpm = false
let isOnlyNuget = false
let isCheckAll = false

let consoleNode = document.getElementById('box-abp-cli-update').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('update-execute')
const selectFileBtn = document.getElementById('update-select-folder-btn')
const includePreviewsCheckbox = document.getElementById('update-include-previews')
const onlyNpmCheckbox = document.getElementById('update-only-npm')
const onlyNugetCheckbox = document.getElementById('update-only-nuget')
const checkAllCheckbox = document.getElementById('update-check-all')

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

checkAllCheckbox.addEventListener('click', (event) => {
  isCheckAll = checkAllCheckbox.checked
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let cmdPath = document.getElementById('update-folder').value
  let solutionName = document.getElementById('update-solution-name').value
  if (isRunning || !cmdPath) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('update-process').style.display = 'block'

  let cmdStr = 'abp update'
  if (isIncludePreviews) cmdStr += ' -p'
  if (isOnlyNpm) cmdStr += ' --npm'
  if (isOnlyNuget) cmdStr += ' --nuget'
  if (solutionName) cmdStr += ' -sn ' + addDoubleQuote(solutionName)
  if (isCheckAll) cmdStr += ' --check-all'
  clearConsoleContent()
  addConsoleContent(cmdStr + '\n\nRunning...\n')
  scrollConsoleToBottom()
  console.log(cmdStr)
  if (process.platform === 'win32') cmdStr = '@chcp 65001 >nul & cmd /d/s/c ' + cmdStr
  workerProcess = exec(cmdStr, {cwd: cmdPath})
  
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