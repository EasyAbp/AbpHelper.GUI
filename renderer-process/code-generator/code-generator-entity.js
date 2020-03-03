const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let uiFramework = 'mvc'

let extraOptions = {
  separateDto: false,
  repository: false,
  skipDbMigrations: false,
  skipUi: false,
  skipLocalization: false,
  skipTest: false,
  noOverwirte: false,
  migrationProjectName: false
}

let consoleNode = document.getElementById('box-code-generator-entity').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('entity-execute')
const selectSolutionFileBtn = document.getElementById('entity-select-solution-file-btn')
const uiRadios = document.getElementsByName('entity-uiFramework-data')
const extraOptionsCheckBox = {
  separateDto: document.getElementById('entity-options-separateDto'),
  repository: document.getElementById('entity-options-repository'),
  skipDbMigrations: document.getElementById('entity-options-skipDbMigrations'),
  skipUi: document.getElementById('entity-options-skipUi'),
  skipLocalization: document.getElementById('entity-options-skipLocalization'),
  skipTest: document.getElementById('entity-options-skipTest'),
  noOverwirte: document.getElementById('entity-options-noOverwirte'),
  migrationProjectName: document.getElementById('entity-options-migrationProjectName'),
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

uiRadios.forEach(function (uiRadio) {
  uiRadio.addEventListener('click', (event) => {
    switch (uiRadio.id) {
      case 'entity-uiFramework-data-mvc':
        uiFramework = 'mvc'
        break;
      case 'entity-uiFramework-data-angular':
        uiFramework = 'angular'
        break;
      case 'entity-uiFramework-data-none':
        uiFramework = 'none'
        break;
      default:
        break;
    }
  })
})

function findLastStr(str, cha, num) {
  let times = num == 0 ? 1 : num;
  var x = str.lastIndexOf(cha);
  for (var i = 0; i < times - 1; i++) {
      x = str.lastIndexOf(cha, x - 1);
  }
  return x;
}

function getSolutionRootPath(slnFilePath) {
  let separator = slnFilePath.indexOf('/') != -1 ? '/' : '\\'
  return uiFramework == 'angular' ? slnFilePath.substr(0, findLastStr(slnFilePath, separator, 2)) : slnFilePath.substr(0, findLastStr(slnFilePath, separator, 1))
}

execBtn.addEventListener('click', (event) => {
  runExec()
})

extraOptionsCheckBox.separateDto.addEventListener('click', (event) => {
  extraOptions.separateDto = extraOptionsCheckBox.separateDto.checked
})
extraOptionsCheckBox.repository.addEventListener('click', (event) => {
  extraOptions.repository = extraOptionsCheckBox.repository.checked
})
extraOptionsCheckBox.skipDbMigrations.addEventListener('click', (event) => {
  extraOptions.skipDbMigrations = extraOptionsCheckBox.skipDbMigrations.checked
})
extraOptionsCheckBox.skipUi.addEventListener('click', (event) => {
  extraOptions.skipUi = extraOptionsCheckBox.skipUi.checked
})
extraOptionsCheckBox.skipLocalization.addEventListener('click', (event) => {
  extraOptions.skipLocalization = extraOptionsCheckBox.skipLocalization.checked
})
extraOptionsCheckBox.skipTest.addEventListener('click', (event) => {
  extraOptions.skipTest = extraOptionsCheckBox.skipTest.checked
})
extraOptionsCheckBox.noOverwirte.addEventListener('click', (event) => {
  extraOptions.noOverwirte = extraOptionsCheckBox.noOverwirte.checked
})
extraOptionsCheckBox.migrationProjectName.addEventListener('click', (event) => {
  extraOptions.migrationProjectName = extraOptionsCheckBox.migrationProjectName.checked
})

function runExec() {
  let entityName = document.getElementById('entity-entity-name').value
  let solutionFile = document.getElementById('entity-solution-file').value
  if (isRunning  || !entityName || !solutionFile) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('entity-process').style.display = 'block'

  let cmdStr = 'abphelper generate crud ' + entityName + ' -d ' + getSolutionRootPath(solutionFile)
  if (extraOptions.separateDto) cmdStr += ' --separate-dto'
  if (extraOptions.repository) cmdStr += ' --custom-repository'
  if (extraOptions.skipDbMigrations) cmdStr += ' --skip-db-migrations'
  if (extraOptions.skipUi) cmdStr += ' --skip-ui'
  if (extraOptions.skipLocalization) cmdStr += ' --skip-localization'
  if (extraOptions.skipTest) cmdStr += ' --skip-test'
  if (extraOptions.noOverwirte) cmdStr += ' --no-overwrite'
  if (extraOptions.migrationProjectName) cmdStr += ' --migration-project-name ' + document.getElementById('entity-options-migrationProjectName.data').value
  clearConsoleContent()
  addConsoleContent(cmdStr + '\n\nRunning...\n')
  scrollConsoleToBottom()
  console.log(cmdStr)
  workerProcess = exec('chcp 65001 & ' + cmdStr, {cwd: '/'})
  
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