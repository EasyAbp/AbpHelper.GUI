const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let isAllValues = false

let consoleNode = document.getElementById('box-abp-cli-translate-create').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('translate-create-execute')
const directorySelectBtn = document.getElementById('translate-create-directory-selectBtn')
const allValuesCheckbox = document.getElementById('translate-create-all-values')

execBtn.addEventListener('click', (event) => {
  runExec()
})

allValuesCheckbox.addEventListener('click', (event) => {
  isAllValues = allValuesCheckbox.checked
})

directorySelectBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('translate-create-directory').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let culture = document.getElementById('translate-create-culture').value
  let directory = document.getElementById('translate-create-directory').value
  let referenceCulture = document.getElementById('translate-create-reference-culture').value
  let output = document.getElementById('translate-create-output').value
  if (isRunning || !culture || !directory) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('translate-create-process').style.display = 'block'

  let cmdStr = 'abp translate -c ' + addDoubleQuote(culture)
  if (referenceCulture) cmdStr += ' -r ' + addDoubleQuote(referenceCulture)
  if (output) cmdStr += ' -o ' + addDoubleQuote(output)
  if (isAllValues) cmdStr += ' -all'
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