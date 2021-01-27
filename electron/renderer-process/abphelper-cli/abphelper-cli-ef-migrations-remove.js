const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let extraOptions = {
  noOverwrite: false
}

let consoleNode = document.getElementById('box-abphelper-cli-ef-migrations-remove').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('ef-migrations-remove-execute')
const selectSolutionFileBtn = document.getElementById('ef-migrations-remove-select-solution-file-btn')
const extraOptionsCheckBox = {
  noOverwrite: document.getElementById('ef-migrations-remove-options-noOverwrite')
}

selectSolutionFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Solution', extensions: ['sln'] },
    ],
    properties: ['openFile']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('ef-migrations-remove-solution-file').value = result.filePaths[0]
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

extraOptionsCheckBox.noOverwrite.addEventListener('click', (event) => {
  extraOptions.noOverwrite = extraOptionsCheckBox.noOverwrite.checked
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let solutionFile = document.getElementById('ef-migrations-remove-solution-file').value
  let migrationProjectName = document.getElementById('ef-migrations-remove-options-migrationProjectName').value
  let exclude = document.getElementById('ef-migrations-remove-options-exclude').value
  let efOptions = document.getElementById('ef-migrations-remove-options-efOptions').value
  if (isRunning || !solutionFile) return
  
  let solutionRootPath = getSolutionRootPath(solutionFile)
  if (!solutionRootPath) return

  isRunning = true
  execBtn.disabled = true
  document.getElementById('ef-migrations-remove-process').style.display = 'block'

  let cliCommand = process.platform === 'win32' ? '%USERPROFILE%\\.dotnet\\tools\\abphelper' : '$HOME/.dotnet/tools/abphelper'
  let cmdStr = cliCommand + ' ef migrations remove -d ' + addDoubleQuote(solutionRootPath)
  if (extraOptions.noOverwrite) cmdStr += ' --no-overwrite'
  if (migrationProjectName) cmdStr += ' --migration-project-name ' + addDoubleQuote(migrationProjectName)
  if (exclude) cmdStr += ' --exclude ' + exclude
  if (efOptions) cmdStr += ' ' + efOptions
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