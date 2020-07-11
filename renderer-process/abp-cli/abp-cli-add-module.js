const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let consoleNode = document.getElementById('box-abp-cli-add-module').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('add-module-execute')
const selectFileBtn = document.getElementById('add-module-select-file-btn')
const selectSpFileBtn = document.getElementById('add-module-startup-project-selectBtn')

execBtn.addEventListener('click', (event) => {
  runExec()
})

selectFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Solution', extensions: ['sln'] },
    ],
    properties: ['openFile']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('add-module-solution-file').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

selectSpFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Project', extensions: ['csproj'] },
    ],
    properties: ['openFile']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('add-module-startup-project').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let moduleName = '"' + document.getElementById('add-module-name').value + '"'
  let file = '"' + document.getElementById('add-module-solution-file').value + '"'
  let startupProject = '"' + document.getElementById('add-module-startup-project').value + '"'
  if (isRunning || !moduleName || !file) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('add-module-process').style.display = 'block'

  let cmdStr = 'abp add-module ' + addDoubleQuote(moduleName) + ' -s ' + addDoubleQuote(file)
  if (document.getElementById('add-module-skip-db-migrations').checked) cmdStr += ' --skip-db-migrations'
  console.log(startupProject)
  if (startupProject) cmdStr += ' -sp ' + addDoubleQuote(startupProject)
  clearConsoleContent()
  addConsoleContent(cmdStr + '\n\nRunning...\n')
  scrollConsoleToBottom()
  console.log(cmdStr)
  if (process.platform === 'win32') cmdStr = '@chcp 65001 >nul & cmd /d/s/c ' + cmdStr
  workerProcess = exec(cmdStr, {cwd: '/'})
  
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