const exec = require('child_process').exec

let isRunning = false

let installConsoleNode = document.getElementById('box-modules-manager-abphelper-cli-install').getElementsByTagName('textarea')[0]
let updateConsoleNode = document.getElementById('box-modules-manager-abphelper-cli-update').getElementsByTagName('textarea')[0]

const installExecBtn = document.getElementById('abphelper-cli-install-execute')
const updateExecBtn = document.getElementById('abphelper-cli-update-execute')

installExecBtn.addEventListener('click', (event) => {
  runExec('install')
})

updateExecBtn.addEventListener('click', (event) => {
  runExec('update')
})

function runExec(action) {
  if (isRunning) return
  isRunning = true

  let execBtn, consoleNode
  if (action === 'install') {
    execBtn = installExecBtn
    consoleNode = installConsoleNode
  } else if (action === 'update') {
    execBtn = updateExecBtn
    consoleNode = updateConsoleNode
  } else {
    return
  }

  execBtn.disabled = true
  document.getElementById('abphelper-cli-' + action + '-process').style.display = 'block'

  let cmdStr = 'dotnet tool ' + action + ' -g EasyAbp.AbpHelper.Cli'
  clearConsoleContent()
  addConsoleContent('Running...\n')
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