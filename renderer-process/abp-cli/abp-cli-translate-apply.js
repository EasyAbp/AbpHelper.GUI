const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let consoleNode = document.getElementById('box-abp-cli-translate-apply').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('translate-apply-execute')
const directorySelectBtn = document.getElementById('translate-apply-directory-selectBtn')

execBtn.addEventListener('click', (event) => {
  runExec()
})

directorySelectBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('translate-apply-directory').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let directory = document.getElementById('translate-apply-directory').value
  let fileName = document.getElementById('translate-apply-file-name').value
  if (isRunning || !directory) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('translate-apply-process').style.display = 'block'

  let cmdStr = 'abp translate -a'
  if (fileName) cmdStr += ' -f ' + addDoubleQuote(fileName)
  clearConsoleContent()
  addConsoleContent(cmdStr + '\n\nRunning...\n')
  scrollConsoleToBottom()
  console.log(cmdStr)
  if (process.platform === 'win32') cmdStr = '@chcp 65001 >nul & cmd /d/s/c ' + cmdStr
  workerProcess = exec(cmdStr, {cwd: directory})
  
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