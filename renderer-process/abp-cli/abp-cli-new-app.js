const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let ui = 'mvc'
let dbProvider = 'ef'
let isTiered = false
let isSeparate = false

let consoleNode = document.getElementById('box-abp-cli-new-app').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('app-execute')
const uiRadios = document.getElementsByName('app-ui')
const dbRadios = document.getElementsByName('app-db')
const selectFolderBtn = document.getElementById('app-select-folder-btn')
const tieredCheckbox = document.getElementById('app-tiered')
const separateCheckbox = document.getElementById('app-options-separate')

selectFolderBtn.addEventListener('click', (event) => {
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

execBtn.addEventListener('click', (event) => {
  runExec()
})

tieredCheckbox.addEventListener('click', (event) => {
  isTiered = tieredCheckbox.checked
})

separateCheckbox.addEventListener('click', (event) => {
  isSeparate = separateCheckbox.checked
})

uiRadios.forEach(function (uiRadio) {
  uiRadio.addEventListener('click', (event) => {
    switch (uiRadio.id) {
      case 'app-ui-mvc':
        ui = 'mvc'
        document.getElementById('app-options-tiered').style.display = 'block'
        document.getElementById('app-options-separate').style.display = 'none'
        break;
      case 'app-ui-angular':
        ui = 'angular'
        document.getElementById('app-options-tiered').style.display = 'none'
        document.getElementById('app-options-separate').style.display = 'block'
        break;
      case 'app-ui-none':
        ui = 'none'
        document.getElementById('app-options-tiered').style.display = 'none'
        document.getElementById('app-options-separate').style.display = 'block'
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
      case 'app-db-mangodb':
        dbProvider = 'mangodb'
        break;
      default:
        break;
    }
  })
})

function runExec() {
  let projName = document.getElementById('app-project-name').value
  let cmdPath = document.getElementById('app-project-folder').value
  let abpVersion = document.getElementById('app-project-version').value
  if (isRunning || !projName || !cmdPath || !abpVersion || !ui || !dbProvider) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('app-process').style.display = 'block'

  let cmdStr = 'abp new ' + projName + ' -t app -u ' + ui
  if (ui === 'mvc' && isTiered) cmdStr += ' --tiered'
  else if (ui === 'angular' || ui === 'none' && isSeparate) cmdStr += ' --separate-identity-server'
  cmdStr += ' -d ' + dbProvider
  if (abpVersion.trim() !== 'latest') cmdStr += ' -v ' + abpVersion
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