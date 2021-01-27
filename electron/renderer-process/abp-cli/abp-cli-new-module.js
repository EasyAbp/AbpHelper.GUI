const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let isNoUi = false
let isCreateSolutionFolder = false

let consoleNode = document.getElementById('box-abp-cli-new-module').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('module-execute')
const projectFolderSelectBtn = document.getElementById('module-project-folder-selectBtn')
const templateSourceSelectBtn = document.getElementById('module-template-source-selectBtn')
const abpPathSelectBtn = document.getElementById('module-abp-path-selectBtn')
const noUiCheckbox = document.getElementById('module-no-ui')
const createSolutionFolderCheckbox = document.getElementById('module-create-solution-folder')

projectFolderSelectBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('module-project-folder').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

templateSourceSelectBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('module-template-source').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

abpPathSelectBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('module-abp-path').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

execBtn.addEventListener('click', (event) => {
  runExec()
})

noUiCheckbox.addEventListener('click', (event) => {
  isNoUi = noUiCheckbox.checked
})

createSolutionFolderCheckbox.addEventListener('click', (event) => {
  isCreateSolutionFolder = createSolutionFolderCheckbox.checked
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let solutionName = document.getElementById('module-solution-name').value
  let cmdPath = document.getElementById('module-project-folder').value
  let abpVersion = document.getElementById('module-abp-version').value
  let templateSource = document.getElementById('module-template-source').value
  let connectionString = document.getElementById('module-connection-string').value
  let abpPath = document.getElementById('module-abp-path').value
  if (isRunning || !solutionName || !cmdPath) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('module-process').style.display = 'block'

  let cmdStr = 'abp new ' + addDoubleQuote(solutionName) + ' -t module'
  if (isNoUi) cmdStr += ' --no-ui'
  if (abpVersion) cmdStr += ' -v ' + addDoubleQuote(abpVersion)
  if (templateSource) cmdStr += ' -ts ' + addDoubleQuote(templateSource)
  if (isCreateSolutionFolder) cmdStr += ' -csf true'
  if (connectionString) cmdStr += ' -cs ' + addDoubleQuote(connectionString)
  if (abpPath) cmdStr += ' --local-framework-ref --abp-path ' + addDoubleQuote(abpPath)
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