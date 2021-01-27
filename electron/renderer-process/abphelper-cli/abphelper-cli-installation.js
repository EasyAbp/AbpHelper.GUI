const exec = require('child_process').exec

let isRunning = false

let installConsoleNode = document.getElementById('box-abphelper-cli-installation-install').getElementsByTagName('textarea')[0]
let updateConsoleNode = document.getElementById('box-abphelper-cli-installation-update').getElementsByTagName('textarea')[0]
let uninstallConsoleNode = document.getElementById('box-abphelper-cli-installation-uninstall').getElementsByTagName('textarea')[0]

const installExecBtn = document.getElementById('abphelper-cli-installation-install-execute')
const updateExecBtn = document.getElementById('abphelper-cli-installation-update-execute')
const uninstallExecBtn = document.getElementById('abphelper-cli-installation-uninstall-execute')

installExecBtn.addEventListener('click', (event) => {
  runExec('install')
})

updateExecBtn.addEventListener('click', (event) => {
  runExec('update')
})

uninstallExecBtn.addEventListener('click', (event) => {
  runExec('uninstall')
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec(action) {
  if (isRunning) return
  isRunning = true

  let execBtn, consoleNode, version
  if (action === 'install') {
    execBtn = installExecBtn
    consoleNode = installConsoleNode
    version = document.getElementById('abphelper-cli-installation-install-version').value
  } else if (action === 'update') {
    execBtn = updateExecBtn
    consoleNode = updateConsoleNode
    version = document.getElementById('abphelper-cli-installation-update-version').value
  } else if (action === 'uninstall') {
    execBtn = uninstallExecBtn
    consoleNode = uninstallConsoleNode
    version = 'latest'
  } else {
    return
  }

  execBtn.disabled = true
  document.getElementById('abphelper-cli-installation-' + action + '-process').style.display = 'block'

  let cmdStr = 'dotnet tool ' + action + ' -g EasyAbp.AbpHelper'
  if (version.trim() !== 'latest') cmdStr += ' --version ' + addDoubleQuote(version)
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