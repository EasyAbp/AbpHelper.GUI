const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let consoleNode = document.getElementById('box-abp-cli-add-package').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('add-package-execute')
const selectFileBtn = document.getElementById('add-package-select-file-btn')

selectFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Project', extensions: ['csproj'] },
    ],
    properties: ['openFile']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('add-package-project-file').value = result.filePaths[0]
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
  let packageName = document.getElementById('add-package-name').value
  let file = document.getElementById('add-package-project-file').value
  if (isRunning || !packageName || !file) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('add-package-process').style.display = 'block'

  let cmdStr = 'abp add-package ' + addDoubleQuote(packageName) + ' -p ' + addDoubleQuote(file)
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