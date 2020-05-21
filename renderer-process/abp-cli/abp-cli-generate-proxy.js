const exec = require('child_process').exec

let isRunning = false

let consoleNode = document.getElementById('box-abp-cli-generate-proxy').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('generate-proxy-execute')


execBtn.addEventListener('click', (event) => {
  runExec()
})

function runExec() {
  let apiUrl = document.getElementById('generate-proxy-api-url').value
  let ui = document.getElementById('generate-proxy-ui').value
  let module = document.getElementById('generate-proxy-module').value
  if (isRunning || !apiUrl) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('generate-proxy-process').style.display = 'block'

  let cmdStr = 'abp generate-proxy'
  if (apiUrl) cmdStr += ' -a ' + apiUrl
  if (ui) cmdStr += ' -u ' + ui
  if (module) cmdStr += ' -m ' + module
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