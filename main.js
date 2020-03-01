// require('update-electron-app')({
//   logger: require('electron-log')
// })

const fetch = require('electron-main-fetch')
const path = require('path')
const glob = require('glob')
const execFile = require('child_process').execFile
const {app, Menu, Tray, BrowserWindow, shell} = require('electron')

const debug = /--debug/.test(process.argv[2])

if (process.mas) app.setName('Abp Helper')

let mainWindow = null
let contextMenu = null

let forceQuit = false

function initialize () {
  makeSingleInstance()

  loadDemos()

  function createTray() {
    tray = new Tray(path.join(__dirname, '/assets/app-icon/png/32.png'))
    buildTrayMenuFromTemplate()
    checkForUpdate()
    refreshAbphelperCliVersion()

    tray.on('double-click', function () {
      if (mainWindow) {
        if (mainWindow.isVisible()) {
          mainWindow.hide()
        } else {
          mainWindow.show()
        }
      }
    })
  }

  function createWindow () {
    const windowOptions = {
      width: 1080,
      minWidth: 680,
      height: 840,
      title: app.getName(),
      webPreferences: {
        nodeIntegration: true
      },
      icon: path.join(__dirname, '/assets/app-icon/png/32.png')
    }

    if (process.platform === 'linux') {
      windowOptions.icon = path.join(__dirname, '/assets/app-icon/png/512.png')
    }

    mainWindow = new BrowserWindow(windowOptions)
    mainWindow.setMenuBarVisibility(false)
    mainWindow.loadURL(path.join('file://', __dirname, '/index.html'))

    // Launch fullscreen with DevTools open, usage: npm run debug
    if (debug) {
      mainWindow.webContents.openDevTools()
      mainWindow.maximize()
      require('devtron').install()
    }

    mainWindow.on('closed', () => {
      mainWindow = null
    })
    mainWindow.on('close', (event) => { 
      mainWindow.hide(); 
      if (!forceQuit) event.preventDefault();
    });
    mainWindow.on('show', () => {
      tray.setHighlightMode('always')
    })
    mainWindow.on('hide', () => {
      tray.setHighlightMode('never')
    })
  }

  app.on('ready', () => {
    createTray()
    createWindow()
  })

  app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
      app.quit()
    }
  })

  app.on('activate', () => {
    if (mainWindow === null) {
      createWindow()
    }
  })
}

let tray = null

let checkUpdateMenuItem = {
  id: 'checkUpdate',
  label: 'GUI: Ready for update checking',
  enabled: false,
  click: async () => await checkForUpdate()
}

let cliCheckUpdateMenuItem = {
  id: 'cliCheckUpdate',
  label: 'CLI: Ready for update checking',
  enabled: true,
  click: async () => refreshAbphelperCliVersion()
}

let downloadReleaseMenuItem = {
  id: 'downloadRelease',
  label: 'Download AbpHelper GUI latest release',
  visible: false,
  click: () => shell.openExternal('https://github.com/EasyAbp/AbpHelper.GUI/releases')
}

let cliUpdateMenuItem = {
  id: 'cliUpdate',
  label: 'Update AbpHelper CLI...',
  visible: false,
  click: () => loadShowPage('modules-manager-abphelper-cli')
}

let template = [{
  label: 'Abp-CLI...',
  click: () => loadShowPage('abp-cli-new')
}, {
  label: 'Code Generator...',
  click: () => loadShowPage('code-generator-entity')
}, {
  label: 'Modules Manager...',
  click: () => loadShowPage('modules-manager-store')
}, {
  label: 'Awesome tools...',
  click: () => loadShowPage('awesome-tools-ef-provider')
}, {
  type: 'separator'
}, {
  label: 'Help',
  submenu: [{
    label: 'Resources',
    submenu: [{
      label: 'Abp Framework',
      click: () => shell.openExternal('https://abp.io')
    }, {
      label: 'Abp Commercial',
      click: () => shell.openExternal('https://commercial.abp.io')
    }, {
      label: 'Abp Helper',
      click: () => shell.openExternal('https://github.com/EasyAbp/AbpHelper.GUI')
    }]
  },
  checkUpdateMenuItem,
  cliCheckUpdateMenuItem,
  downloadReleaseMenuItem,
  cliUpdateMenuItem,
  {
    label: 'About...',
    click: () => loadShowPage('about')
  }]
}, {
  label: 'Quit',
  click: () => {
    forceQuit = true
    app.quit()
  }
}]

function loadShowPage(tag) {
  mainWindow.loadURL(path.join('file://', __dirname, '/index.html#' + tag))
  mainWindow.show()
}

function buildTrayMenuFromTemplate() {
  contextMenu = Menu.buildFromTemplate(template)
  tray.setToolTip('Abp Helper')
  tray.setContextMenu(contextMenu)
}

async function checkForUpdate() {
  const currentVersion = app.getVersion()
  downloadReleaseMenuItem.visible = false
  checkUpdateMenuItem.label = 'GUI: Checking for Update....'
  checkUpdateMenuItem.enabled = false
  const data = JSON.parse(await fetch('https://api.github.com/repos/EasyAbp/AbpHelper.GUI/releases/latest', {type: 'text'}))
  if (data.tag_name) {
    checkUpdateMenuItem.label = 'GUI: Latest: v' + data.tag_name + ' (Current: v' + currentVersion + ')'
    checkUpdateMenuItem.enabled = true
    if (currentVersion != data.tag_name) {
      downloadReleaseMenuItem.visible = true
    }
  } else if (data.message && data.message.indexOf('API rate limit exceeded') == 0) {
    checkUpdateMenuItem.label = 'GUI: Update checking failed (API rate limit exceeded)'
    checkUpdateMenuItem.enabled = true
  } else {
    checkUpdateMenuItem.label = 'GUI: Update checking failed'
    checkUpdateMenuItem.enabled = true
  }
  buildTrayMenuFromTemplate()
}

async function cliCheckForUpdate() {
  const currentVersion = getAbphelperCliVersion()
  downloadReleaseMenuItem.visible = false
  cliCheckUpdateMenuItem.label = 'CLI: Checking for CLI Update....'
  cliCheckUpdateMenuItem.enabled = false
  const data = JSON.parse(await fetch('https://api.github.com/repos/EasyAbp/AbpHelper.CLI/releases/latest', {type: 'text'}))
  if (data.tag_name) {
    cliCheckUpdateMenuItem.label = 'CLI: Latest: v' + data.tag_name + ' (Current: v' + currentVersion + ')'
    cliCheckUpdateMenuItem.enabled = true
    if (currentVersion != data.tag_name) {
      cliUpdateMenuItem.visible = true
    }
  } else if (data.message && data.message.indexOf('API rate limit exceeded') == 0) {
    cliCheckUpdateMenuItem.label = 'CLI: Update checking failed (API rate limit exceeded)'
    cliCheckUpdateMenuItem.enabled = true
  } else {
    cliCheckUpdateMenuItem.label = 'CLI: Update checking failed'
    cliCheckUpdateMenuItem.enabled = true
  }
  buildTrayMenuFromTemplate()
}

// Make this app a single instance app.
//
// The main window will be restored and focused instead of a second window
// opened when a person attempts to launch a second instance.
//
// Returns true if the current version of the app should quit instead of
// launching.
function makeSingleInstance () {
  if (process.mas) return

  app.requestSingleInstanceLock()

  app.on('second-instance', () => {
    if (mainWindow) {
      if (mainWindow.isMinimized()) mainWindow.restore()
      mainWindow.focus()
    }
  })
}

// Require each JS file in the main-process dir
function loadDemos () {
  const files = glob.sync(path.join(__dirname, 'main-process/**/*.js'))
  files.forEach((file) => { require(file) })
}

let abphelperCliVersion = null

function getAbphelperCliVersion() {
  return abphelperCliVersion
}

function refreshAbphelperCliVersion() {
  workerProcess = execFile('abphelper', ['--version'], (error, stdout, stderr) => {
    if (error) {
      abphelperCliVersion = null
      console.log(error)
      console.log(stderr)
      return
    }
    let version = stdout.replace(/[\r\n]/g, "")
    abphelperCliVersion = version
    cliCheckForUpdate()
  })
}

initialize()
