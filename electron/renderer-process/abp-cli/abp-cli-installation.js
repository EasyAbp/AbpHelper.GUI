const exec = require('child_process').exec

let isRunning = false

let installConsoleNode = document.getElementById('box-abp-cli-cli-install').getElementsByTagName('textarea')[0]
let updateConsoleNode = document.getElementById('box-abp-cli-cli-update').getElementsByTagName('textarea')[0]
let uninstallConsoleNode = document.getElementById('box-abp-cli-cli-uninstall').getElementsByTagName('textarea')[0]

const installExecBtn = document.getElementById('cli-install-execute')
const updateExecBtn = document.getElementById('cli-update-execute')
const uninstallExecBtn = document.getElementById('cli-uninstall-execute')

installExecBtn.addEventListener('click', (event) => {
  runExec('install')
})

updateExecBtn.addEventListener('click', (event) => {
  runExec('update')
})

uninstallExecBtn.addEventListener('click', (event) => {
  runExec('uninstall')
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
  } else if (action === 'uninstall') {
    execBtn = uninstallExecBtn
    consoleNode = uninstallConsoleNode
  } else {
    return
  }

  execBtn.disabled = true
  document.getElementById('cli-' + action + '-process').style.display = 'block'

  let cmdStr = 'dotnet tool ' + action + ' -g Volo.Abp.Cli'
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