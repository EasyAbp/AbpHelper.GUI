const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let consoleNode = document.getElementById('box-abp-cli-remove-proxy').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('remove-proxy-execute')
const projectSelectBtn = document.getElementById('remove-proxy-project-selectBtn')

execBtn.addEventListener('click', (event) => {
  runExec()
})

projectSelectBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('remove-proxy-project').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let cmdPath = document.getElementById('remove-proxy-project').value
  let module = document.getElementById('remove-proxy-module').value
  let apiName = document.getElementById('remove-proxy-api-name').value
  let source = document.getElementById('remove-proxy-source').value
  let target = document.getElementById('remove-proxy-target').value
  let prompt = document.getElementById('remove-proxy-prompt').value
  if (isRunning || !cmdPath) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('remove-proxy-process').style.display = 'block'

  let cmdStr = 'abp remove-proxy'
  if (module) cmdStr += ' -m ' + addDoubleQuote(module)
  if (apiName) cmdStr += ' -a ' + addDoubleQuote(apiName)
  if (source) cmdStr += ' -s ' + addDoubleQuote(source)
  if (target) cmdStr += ' -t ' + addDoubleQuote(target)
  if (prompt) cmdStr += ' -p ' + addDoubleQuote(prompt)
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