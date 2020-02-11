const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let basicOptions = {
  dbcontext : true,
  configure : true,
  addMigration : true,
  updateDatabase : true,
  repository : false,
  dto : true,
  mapping : true,
  appservice : true,
  localization : true,
  test : true
}

let uiOptions = {
  mvc: {
    menu: true,
    mapping: true,
    pageIndex: true,
    pageCreation: true,
    pageEdit: true
  }
}

let consoleNode = document.getElementById('box-code-generator-entity').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('entity-execute')
const selectSolutionFileBtn = document.getElementById('entity-select-solution-file-btn')
const basicOptionsCheckBox = {
  dbcontext: document.getElementById('entity-options-basic-dbcontext'),
  configure: document.getElementById('entity-options-basic-configure'),
  addMigration: document.getElementById('entity-options-basic-addMigration'),
  updateDatabase: document.getElementById('entity-options-basic-updateDatabase'),
  dto: document.getElementById('entity-options-basic-dto'),
  mapping: document.getElementById('entity-options-basic-mapping'),
  appservice: document.getElementById('entity-options-basic-appservice'),
  localization: document.getElementById('entity-options-basic-localization'),
  test: document.getElementById('entity-options-basic-test')
}
const mvcUiOptionsCheckBox = {
  menu: document.getElementById('entity-options-ui-mvc-menu'),
  mapping: document.getElementById('entity-options-ui-mvc-mapping'),
  pageIndex: document.getElementById('entity-options-ui-mvc-pageIndex'),
  pageCreation: document.getElementById('entity-options-ui-mvc-pageCreation'),
  pageEdit: document.getElementById('entity-options-ui-mvc-pageEdit')
}

selectSolutionFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Solution', extensions: ['sln'] },
    ],
    properties: ['openFile']
  }, (files) => {
    if (files) {
      document.getElementById('entity-solution-file').value = files[0]
    }
  })
})

execBtn.addEventListener('click', (event) => {
  runExec()
})

basicOptionsCheckBox.dbcontext.addEventListener('click', (event) => {
  basicOptions.dbcontext = basicOptionsCheckBox.dbcontext.checked
})

function runExec() {
  let entityName = document.getElementById('entity-entity-name').value
  let solutionFile = document.getElementById('entity-solution-file').value
  if (isRunning  || !entityName || !solutionFile) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('entity-process').style.display = 'block'

  let cmdStr = 'abphelper generate ' + entityName + ' -s ' + solutionFile
  clearConsoleContent()
  addConsoleContent('Running...\n')
  scrollConsoleToBottom()
  console.log(cmdStr)
  workerProcess = exec('chcp 65001 & ' + cmdStr)
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