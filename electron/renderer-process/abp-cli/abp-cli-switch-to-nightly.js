const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let consoleNode = document.getElementById('box-abp-cli-switch-to-nightly').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('switch-to-nightly-execute')
const solutionDirectorySelectBtn = document.getElementById('switch-to-nightly-solution-directory-selectBtn')

solutionDirectorySelectBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('switch-to-nightly-solution-directory').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

execBtn.addEventListener('click', (event) => {
  runExec()
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let solutionDirectory = document.getElementById('switch-to-nightly-solution-directory').value
  if (isRunning || !solutionDirectory) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('switch-to-nightly-process').style.display = 'block'

  let cmdStr = 'abp switch-to-nightly'
  if (solutionDirectory) cmdStr += ' -sd ' + addDoubleQuote(solutionDirectory)
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