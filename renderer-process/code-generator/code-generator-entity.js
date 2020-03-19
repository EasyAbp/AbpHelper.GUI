const { dialog } = require('electron').remote
const exec = require('child_process').exec
const fs = require('fs')

let isRunning = false

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
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('entity-solution-file').value = result.filePaths[0]
    }
  }).catch(err => {
    console.log(err)
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
  let strs = slnFilePath.split(separator)
  if (strs.length > 1 && strs[strs.length - 2] === 'aspnet-core') {
    // is app
    return slnFilePath.substr(0, findLastStr(slnFilePath, separator, 2))
  }
  var moduleRootPath = slnFilePath.substr(0, findLastStr(slnFilePath, separator, 1))
  if (fs.existsSync(moduleRootPath + separator + 'host')) {
    // is module
    return moduleRootPath
  }
  return alert('The .sln file must be in the "aspnet-core" folder for app solution.')
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
  
  let solutionRootPath = getSolutionRootPath(solutionFile)
  if (!solutionRootPath) return

  isRunning = true
  execBtn.disabled = true
  document.getElementById('entity-process').style.display = 'block'

  let cliCommand = process.platform === 'win32' ? '%USERPROFILE%\\.dotnet\\tools\\abphelper' : '$HOME/.dotnet/tools/abphelper'
  let cmdStr = cliCommand + ' generate crud ' + entityName + ' -d ' + solutionRootPath
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