const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let consoleNode = document.getElementById('box-abp-cli-generate-proxy').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('generate-proxy-execute')
const projectSelectBtn = document.getElementById('generate-proxy-project-selectBtn')

execBtn.addEventListener('click', (event) => {
  runExec()
})

projectSelectBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('generate-proxy-project').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let cmdPath = document.getElementById('generate-proxy-project').value
  let apiUrl = document.getElementById('generate-proxy-api-url').value
  let ui = document.getElementById('generate-proxy-ui').value
  let module = document.getElementById('generate-proxy-module').value
  if (isRunning || !cmdPath) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('generate-proxy-process').style.display = 'block'

  let cmdStr = 'abp generate-proxy'
  if (apiUrl) cmdStr += ' -a ' + addDoubleQuote(apiUrl)
  if (ui) cmdStr += ' -u ' + addDoubleQuote(ui)
  if (module) cmdStr += ' -m ' + addDoubleQuote(module)
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