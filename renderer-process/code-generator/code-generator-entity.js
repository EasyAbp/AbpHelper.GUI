const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let extraOptions = {
  separateDto: false,
  repository: false,
  skipDbMigrations: false
}

let consoleNode = document.getElementById('box-code-generator-entity').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('entity-execute')
const selectSolutionFileBtn = document.getElementById('entity-select-solution-file-btn')
const extraOptionsCheckBox = {
  separateDto: document.getElementById('entity-options-separateDto'),
  repository: document.getElementById('entity-options-repository'),
  skipDbMigrations: document.getElementById('entity-options-skipDbMigrations'),
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

extraOptionsCheckBox.separateDto.addEventListener('click', (event) => {
  extraOptions.separateDto = extraOptionsCheckBox.separateDto.checked
})
extraOptionsCheckBox.repository.addEventListener('click', (event) => {
  extraOptions.repository = extraOptionsCheckBox.repository.checked
})
extraOptionsCheckBox.repository.addEventListener('click', (event) => {
  extraOptions.skipDbMigrations = extraOptionsCheckBox.skipDbMigrations.checked
})

function runExec() {
  let entityName = document.getElementById('entity-entity-name').value
  let solutionFile = document.getElementById('entity-solution-file').value
  if (isRunning  || !entityName || !solutionFile) return
  isRunning = true
  execBtn.disabled = true
  document.getElementById('entity-process').style.display = 'block'

  let cmdStr = 'abphelper generate -e ' + entityName + ' -s ' + solutionFile
  if (extraOptions.separateDto) cmdStr += ' --separate-dto'
  if (extraOptions.repository) cmdStr += ' --custom-repository'
  if (extraOptions.skipDbMigrations) cmdStr += ' --skip-db-migrations'
  clearConsoleContent()
  addConsoleContent(cmdStr + '\n\nRunning...\n')
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