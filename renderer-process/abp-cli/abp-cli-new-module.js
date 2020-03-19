const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let isNoUi = false

let consoleNode = document.getElementById('box-abp-cli-new-module').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('module-execute')
const selectFolderBtn = document.getElementById('module-select-folder-btn')
const noUiCheckbox = document.getElementById('module-no-ui')

selectFolderBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('module-project-folder').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

execBtn.addEventListener('click', (event) => {
  runExec()
})

noUiCheckbox.addEventListener('click', (event) => {
  isNoUi = noUiCheckbox.checked
})

function runExec() {
  let projName = document.getElementById('module-project-name').value
  let cmdPath = document.getElementById('module-project-folder').value
  let abpVersion = document.getElementById('module-project-version').value
  if (isRunning || !projName || !cmdPath || !abpVersion) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('module-process').style.display = 'block'

  let cmdStr = 'abp new ' + projName + ' -t module'
  if (isNoUi) cmdStr += ' --no-ui'
  if (abpVersion.trim() !== 'latest') cmdStr += ' -v ' + abpVersion
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