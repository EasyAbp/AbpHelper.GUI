const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let ui = 'mvc'
let mobile = 'none'
let dbProvider = 'ef'
let isTiered = false
let isSeparateIdentityServerCheckbox = false
let isCreateSolutionFolder = false

let consoleNode = document.getElementById('box-abp-cli-new-app').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('app-execute')
const uiRadios = document.getElementsByName('app-ui')
const mobileRadios = document.getElementsByName('app-mobile')
const dbRadios = document.getElementsByName('app-db')
const projectFolderSelectBtn = document.getElementById('app-project-folder-selectBtn')
const templateSourceSelectBtn = document.getElementById('app-template-source-selectBtn')
const abpPathSelectBtn = document.getElementById('app-abp-path-selectBtn')
const tieredCheckbox = document.getElementById('app-tiered')
const separateIdentityServerCheckbox = document.getElementById('app-separate-identity-server')
const createSolutionFolderCheckbox = document.getElementById('app-create-solution-folder')

projectFolderSelectBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    properties: ['openDirectory']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('app-project-folder').value = result.filePaths[0]
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
      document.getElementById('app-template-source').value = result.filePaths[0]
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
      document.getElementById('app-abp-path').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
  })
})

execBtn.addEventListener('click', (event) => {
  runExec()
})

tieredCheckbox.addEventListener('click', (event) => {
  isTiered = tieredCheckbox.checked
})

separateIdentityServerCheckbox.addEventListener('click', (event) => {
  isSeparateIdentityServerCheckbox = separateIdentityServerCheckbox.checked
})

createSolutionFolderCheckbox.addEventListener('click', (event) => {
  isCreateSolutionFolder = createSolutionFolderCheckbox.checked
})

uiRadios.forEach(function (uiRadio) {
  uiRadio.addEventListener('click', (event) => {
    switch (uiRadio.id) {
      case 'app-ui-mvc':
        ui = 'mvc'
        document.getElementById('app-options-tiered').style.display = 'block'
        document.getElementById('app-options-separate-identity-server').style.display = 'none'
        break;
      case 'app-ui-angular':
        ui = 'angular'
        document.getElementById('app-options-tiered').style.display = 'none'
        document.getElementById('app-options-separate-identity-server').style.display = 'block'
        break;
      case 'app-ui-none':
        ui = 'none'
        document.getElementById('app-options-tiered').style.display = 'none'
        document.getElementById('app-options-separate-identity-server').style.display = 'block'
        break;
      default:
        break;
    }
  })
})

mobileRadios.forEach(function (mobileRadio) {
  mobileRadio.addEventListener('click', (event) => {
    switch (mobileRadio.id) {
      case 'app-mobile-none':
        mobile = 'none'
        break;
      case 'app-mobile-react-native':
        mobile = 'react-native'
        break;
      default:
        break;
    }
  })
})

dbRadios.forEach(function (dbRadio) {
  dbRadio.addEventListener('click', (event) => {
    switch (dbRadio.id) {
      case 'app-db-ef':
        dbProvider = 'ef'
        break;
      case 'app-db-mongodb':
        dbProvider = 'mongodb'
        break;
      default:
        break;
    }
  })
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let solutionName = document.getElementById('app-solution-name').value
  let cmdPath = document.getElementById('app-project-folder').value
  let abpVersion = document.getElementById('app-abp-version').value
  let templateSource = document.getElementById('app-template-source').value
  let connectionString = document.getElementById('app-connection-string').value
  let abpPath = document.getElementById('app-abp-path').value
  if (isRunning || !solutionName || !cmdPath || !ui || !mobile || !dbProvider) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('app-process').style.display = 'block'

  let cmdStr = 'abp new ' + addDoubleQuote(solutionName) + ' -t app -u ' + ui
  if (ui === 'mvc' && isTiered) cmdStr += ' --tiered'
  else if ((ui === 'angular' || ui === 'none') && isSeparateIdentityServerCheckbox) cmdStr += ' --separate-identity-server'
  cmdStr += ' -m ' + mobile
  cmdStr += ' -d ' + dbProvider
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