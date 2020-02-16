const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let consoleNode = document.getElementById('box-abp-cli-add-module').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('add-module-execute')
const selectFileBtn = document.getElementById('add-module-select-file-btn')
const selectSpFileBtn = document.getElementById('add-module-select-startup-project-file-btn')
const spCheckbox = document.getElementById('add-module-specified-startup-project')

execBtn.addEventListener('click', (event) => {
  runExec()
})

selectFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Solution', extensions: ['sln'] },
    ],
    properties: ['openFile']
  }, (files) => {
    if (files) {
      document.getElementById('add-module-solution-file').value = files[0]
    }
  })
})

selectSpFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Project', extensions: ['csproj'] },
    ],
    properties: ['openFile']
  }, (files) => {
    if (files) {
      document.getElementById('add-module-startup-project-file').value = files[0]
    }
  })
})

spCheckbox.addEventListener('click', (event) => {
  if (spCheckbox.checked) {
    document.getElementById('add-module-startup-project').style.display = 'block'
  } else {
    document.getElementById('add-module-startup-project').style.display = 'none'
  }
})

function runExec() {
  let moduleName = document.getElementById('add-module-name').value
  let file = document.getElementById('add-module-solution-file').value
  if (isRunning || !moduleName || !file) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('add-module-process').style.display = 'block'

  let cmdStr = 'abp add-module ' + moduleName + ' -s ' + file
  if (document.getElementById('add-module-skip-db-migrations').checked) cmdStr += ' --skip-db-migrations'
  if (spCheckbox.checked) cmdStr += ' -sp ' + document.getElementById('add-module-startup-project-file').value
  clearConsoleContent()
  addConsoleContent(cmdStr + '\n\nRunning...\n')
  scrollConsoleToBottom()
  console.log(cmdStr)
  workerProcess = exec('chcp 65001 & ' + cmdStr, {})
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