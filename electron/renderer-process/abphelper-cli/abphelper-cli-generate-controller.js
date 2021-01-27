const { dialog } = require('electron').remote
const exec = require('child_process').exec

let isRunning = false

let extraOptions = {
  skipBuild: false,
  regenerate: false
}

let consoleNode = document.getElementById('box-abphelper-cli-generate-controller').getElementsByTagName('textarea')[0]

const execBtn = document.getElementById('ctrl-execute')
const selectSolutionFileBtn = document.getElementById('ctrl-select-solution-file-btn')
const extraOptionsCheckBox = {
  skipBuild: document.getElementById('ctrl-options-skipBuild'),
  regenerate: document.getElementById('ctrl-options-regenerate')
}

selectSolutionFileBtn.addEventListener('click', (event) => {
  dialog.showOpenDialog({
    filters: [
      { name: 'Abp Solution', extensions: ['sln'] },
    ],
    properties: ['openFile']
  }).then(result => {
    if (result.filePaths[0]) {
      document.getElementById('ctrl-solution-file').value = result.filePaths[0]
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

extraOptionsCheckBox.skipBuild.addEventListener('click', (event) => {
  extraOptions.skipBuild = extraOptionsCheckBox.skipBuild.checked
})
extraOptionsCheckBox.regenerate.addEventListener('click', (event) => {
  extraOptions.regenerate = extraOptionsCheckBox.regenerate.checked
})

function addDoubleQuote(str) {
  return '"' + str + '"'
}

function runExec() {
  let serviceName = document.getElementById('ctrl-service-name').value
  let solutionFile = document.getElementById('ctrl-solution-file').value
  let exclude = document.getElementById('ctrl-options-exclude').value
  if (isRunning || !serviceName || !solutionFile) return
  
  let solutionRootPath = getSolutionRootPath(solutionFile)
  if (!solutionRootPath) return

  isRunning = true
  execBtn.disabled = true
  document.getElementById('ctrl-process').style.display = 'block'

  let cliCommand = process.platform === 'win32' ? '%USERPROFILE%\\.dotnet\\tools\\abphelper' : '$HOME/.dotnet/tools/abphelper'
  let cmdStr = cliCommand + ' generate controller ' + addDoubleQuote(serviceName) + ' -d ' + addDoubleQuote(solutionRootPath)
  if (extraOptions.skipBuild) cmdStr += ' --skip-build'
  if (extraOptions.regenerate) cmdStr += ' --regenerate'
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