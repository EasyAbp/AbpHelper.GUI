const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let extraOptions = {
  noOverwrite: false
}

let consoleNode = document.getElementById('box-abphelper-cli-generate-appService-class').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('appService-class-execute')
const selectSolutionFileBtn = document.getElementById('appService-class-select-solution-file-btn')
const extraOptionsCheckBox = {
  noOverwrite: document.getElementById('appService-class-options-noOverwrite')
}

selectSolutionFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Solution', extensions: ['sln'] },
    ],
    properties: ['openFile']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('appService-class-solution-file').value = result.filePaths[0]
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
  let serviceName = document.getElementById('appService-class-service-name').value
  let solutionFile = document.getElementById('appService-class-solution-file').value
  let folder = document.getElementById('appService-class-options-folder').value
  let exclude = document.getElementById('appService-class-options-exclude').value
  if (isRunning || !serviceName || !solutionFile) return
  
  let solutionRootPath = getSolutionRootPath(solutionFile)
  if (!solutionRootPath) return

  isRunning = true
  execBtn.disabled = true
  document.getElementById('appService-class-process').style.display = 'block'

  let cliCommand = process.platform === 'win32' ? '%USERPROFILE%\\.dotnet\\tools\\abphelper' : '$HOME/.dotnet/tools/abphelper'
  let cmdStr = cliCommand + ' generate service ' + addDoubleQuote(serviceName) + ' -d ' + addDoubleQuote(solutionRootPath)
  if (extraOptions.noOverwrite) cmdStr += ' --no-overwrite'
  if (folder) cmdStr += ' --folder ' + addDoubleQuote(folder)
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