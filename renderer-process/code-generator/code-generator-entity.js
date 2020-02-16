const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let basicOptions = {
  dbcontext : true,
  configure : true,
  repository : false,
  dto : true,
  appservice : true,
  mapping : true,
  localization : true,
  ui : true,
  test : true,
  addMigration : true,
  updateDatabase : true
}

let extraOptions = {
  separateDto : false
}

let uiOptions = {
  mvc: {
    pageIndex: true,
    pageCreation: true,
    pageEdit: true,
    menu: true,
    mapping: true
  }
}

let consoleNode = document.getElementById('box-code-generator-entity').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('entity-execute')
const selectSolutionFileBtn = document.getElementById('entity-select-solution-file-btn')
const basicOptionsCheckBox = {
  dbcontext: document.getElementById('entity-options-basic-dbcontext'),
  configure: document.getElementById('entity-options-basic-configure'),
  repository: document.getElementById('entity-options-basic-repository'),
  dto: document.getElementById('entity-options-basic-dto'),
  appservice: document.getElementById('entity-options-basic-appservice'),
  mapping: document.getElementById('entity-options-basic-mapping'),
  localization: document.getElementById('entity-options-basic-localization'),
  ui: document.getElementById('entity-options-basic-ui'),
  test: document.getElementById('entity-options-basic-test'),
  addMigration: document.getElementById('entity-options-basic-addMigration'),
  updateDatabase: document.getElementById('entity-options-basic-updateDatabase')
}
const extraOptionsCheckBox = {
  separateDto: document.getElementById('entity-options-extra-separateDto'),
}
const mvcUiOptionsCheckBox = {
  pageIndex: document.getElementById('entity-options-uiMvcPageIndex'),
  pageCreation: document.getElementById('entity-options-uiMvcPageCreation'),
  pageEdit: document.getElementById('entity-options-uiMvcPageEdit'),
  menu: document.getElementById('entity-options-ui-mvc-menu'),
  mapping: document.getElementById('entity-options-ui-mvc-mapping')
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

basicOptionsCheckBox.repository.addEventListener('click', (event) => {
  basicOptions.repository = basicOptionsCheckBox.repository.checked
})

extraOptionsCheckBox.separateDto.addEventListener('click', (event) => {
  extraOptions.separateDto = extraOptionsCheckBox.separateDto.checked
})

function runExec() {
  let entityName = document.getElementById('entity-entity-name').value
  let solutionFile = document.getElementById('entity-solution-file').value
  if (isRunning  || !entityName || !solutionFile) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('entity-process').style.display = 'block'

  let cmdStr = 'abphelper generate -e' + entityName + ' -s ' + solutionFile
  if (basicOptions.repository) cmdStr += ' --custom-repository'
  if (extraOptions.separateDto) cmdStr += ' --separate-dto'
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