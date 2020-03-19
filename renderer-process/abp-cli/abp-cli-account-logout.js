const exec = require('child_process').exec

let isRunning = false

let consoleNode = document.getElementById('box-abp-cli-account-logout').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('account-logout-execute')

execBtn.addEventListener('click', (event) => {
  runExec()
})

function runExec() {
  if (isRunning) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('account-logout-process').style.display = 'block'

  let cmdStr = 'abp logout'
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