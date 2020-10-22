const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let extraOptions = {
  shared: true,
  domain: true,
  efcore: true,
  mongodb: false,
  contracts: true,
  application: true,
  httpapi: true,
  client: true,
  web: true
}

let consoleNode = document.getElementById('box-abphelper-cli-module-add').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('module-add-execute')
const selectSolutionFileBtn = document.getElementById('module-add-select-solution-file-btn')
const extraOptionsCheckBox = {
  shared: document.getElementById('module-add-options-shared'),
  domain: document.getElementById('module-add-options-domain'),
  efcore: document.getElementById('module-add-options-efcore'),
  mongodb: document.getElementById('module-add-options-mongodb'),
  contracts: document.getElementById('module-add-options-contracts'),
  application: document.getElementById('module-add-options-application'),
  httpapi: document.getElementById('module-add-options-httpapi'),
  client: document.getElementById('module-add-options-client'),
  web: document.getElementById('module-add-options-web')
}

selectSolutionFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Solution', extensions: ['sln'] },
    ],
    properties: ['openFile']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('module-add-solution-file').value = result.filePaths[0]
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
  return slnFilePath.substr(0, findLastStr(slnFilePath, separator, 1))
}

execBtn.addEventListener('click', (event) => {
  runExec()
})

extraOptionsCheckBox.shared.addEventListener('click', (event) => {
  extraOptions.shared = extraOptionsCheckBox.shared.checked
})

extraOptionsCheckBox.domain.addEventListener('click', (event) => {
  extraOptions.domain = extraOptionsCheckBox.domain.checked
})

extraOptionsCheckBox.efcore.addEventListener('click', (event) => {
  extraOptions.efcore = extraOptionsCheckBox.efcore.checked
})

extraOptionsCheckBox.mongodb.addEventListener('click', (event) => {
  extraOptions.mongodb = extraOptionsCheckBox.mongodb.checked
})

extraOptionsCheckBox.contracts.addEventListener('click', (event) => {
  extraOptions.contracts = extraOptionsCheckBox.contracts.checked
})

extraOptionsCheckBox.application.addEventListener('click', (event) => {
  extraOptions.application = extraOptionsCheckBox.application.checked
})

extraOptionsCheckBox.httpapi.addEventListener('click', (event) => {
  extraOptions.httpapi = extraOptionsCheckBox.httpapi.checked
})

extraOptionsCheckBox.client.addEventListener('click', (event) => {
  extraOptions.client = extraOptionsCheckBox.client.checked
})

extraOptionsCheckBox.web.addEventListener('click', (event) => {
  extraOptions.web = extraOptionsCheckBox.web.checked
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let moduleName = document.getElementById('module-add-moduleName').value
  let solutionFile = document.getElementById('module-add-solution-file').value
  let version = document.getElementById('module-add-options-version').value
  let exclude = document.getElementById('module-add-options-exclude').value
  if (isRunning || !moduleName || !solutionFile) return
  
  let solutionRootPath = getSolutionRootPath(solutionFile)
  if (!solutionRootPath) return

  isRunning = true
  execBtn.disabled = true
  document.getElementById('module-add-process').style.display = 'block'

  let cliCommand = process.platform === 'win32' ? '%USERPROFILE%\\.dotnet\\tools\\abphelper' : '$HOME/.dotnet/tools/abphelper'
  let cmdStr = cliCommand + ' module add ' + moduleName + ' -d ' + addDoubleQuote(solutionRootPath)
  if (extraOptions.shared) cmdStr += ' --shared'
  if (extraOptions.domain) cmdStr += ' --domain'
  if (extraOptions.efcore) cmdStr += ' --entity-framework-core'
  if (extraOptions.mongodb) cmdStr += ' --mongo-db'
  if (extraOptions.contracts) cmdStr += ' --contracts'
  if (extraOptions.application) cmdStr += ' --application'
  if (extraOptions.httpapi) cmdStr += ' --http-api'
  if (extraOptions.client) cmdStr += ' --client'
  if (extraOptions.web) cmdStr += ' --web'
  if (version) cmdStr += ' --version ' + version
  if (exclude) cmdStr += ' --exclude ' + exclude
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