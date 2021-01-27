// require('update-electron-app')({
//   logger: require('electron-log')
// })

const fetch = require('electron-main-fetch')
const path = require('path')
const glob = require('glob')
const exec = require('child_process').exec
const {app, Menu, Tray, BrowserWindow, shell} = require('electron')

const debug = /--debug/.test(process.argv[2])

if (process.mas) app.setName('AbpHelper')

if (process.platform === 'darwin') require('fix-path')()

let mainWindow = null
let contextMenu = null

let forceQuit = false

function initialize () {
  makeSingleInstance()

  loadDemos()

  function createTray() {
    let trayIcon = process.platform === 'darwin' ? '/assets/app-icon/tray/icon-darwin.png' : '/assets/app-icon/tray/icon.png'
    console.log(trayIcon)
    tray = new Tray(path.join(__dirname, trayIcon))
    buildTrayMenuFromTemplate()
    if (!debug) {
      checkForUpdate()
      refreshAbphelperCliVersion()
    }

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
      title: app.name + ' v' + app.getVersion(),
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
      // require('devtron').install()
    }

    mainWindow.on('closed', () => {
      mainWindow = null
    })
    mainWindow.on('close', (event) => { 
      mainWindow.hide(); 
      if (!forceQuit) event.preventDefault();
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
    }else if (process.platform === 'darwin' && !mainWindow.isVisible()) {
      createWindow()
    }
  })

  app.on('before-quit', () => {
    if (process.platform === 'darwin') {
         forceQuit = true;
    }
 });
}

let tray = null

let checkUpdateMenuItem = {
  id: 'checkUpdate',
  label: 'GUI Version: Ready for update checking',
  enabled: false,
  click: async () => await checkForUpdate()
}

let cliCheckUpdateMenuItem = {
  id: 'cliCheckUpdate',
  label: 'CLI Version: Ready for update checking',
  enabled: false,
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
  click: () => loadShowPage('abphelper-cli-installation')
}

let template = [{
  label: 'Abp CLI...',
  click: () => loadShowPage('abp-cli-new')
}, {
  label: 'AbpHelper CLI...',
  click: () => loadShowPage('abphelper-cli-generate-crud')
}, {
  label: 'Modules Manager...',
  click: () => loadShowPage('modules-manager-market')
// }, {
//   label: 'Awesome Tools...',
//   click: () => loadShowPage('awesome-tools-ef-provider')
// }, {
//   type: 'separator'
}, {
  label: 'Help',
  submenu: [{
    label: 'Resources',
    submenu: [{
      label: 'Abp Framework',
      icon: path.join(__dirname, '/assets/app-icon/menuitem/abp/icon.png'),
      click: () => shell.openExternal('https://abp.io')
    }, {
      label: 'Abp Commercial',
      icon: path.join(__dirname, '/assets/app-icon/menuitem/abp/icon.png'),
      click: () => shell.openExternal('https://commercial.abp.io')
    }, {
      label: 'AbpHelper GUI',
      icon: path.join(__dirname, '/assets/app-icon/menuitem/abphelper/icon.png'),
      click: () => shell.openExternal('https://github.com/EasyAbp/AbpHelper.GUI')
    }, {
      label: 'AbpHelper CLI',
      icon: path.join(__dirname, '/assets/app-icon/menuitem/abphelper/icon.png'),
      click: () => shell.openExternal('https://github.com/EasyAbp/AbpHelper.CLI')
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
  app.emit('tray-nav-selected', tag);
  mainWindow.show()
}

function buildTrayMenuFromTemplate() {
  contextMenu = Menu.buildFromTemplate(template)
  tray.setToolTip('AbpHelper v' + app.getVersion())
  tray.setContextMenu(contextMenu)
}

async function checkForUpdate() {
  const currentVersion = app.getVersion()
  downloadReleaseMenuItem.visible = false
  checkUpdateMenuItem.label = 'GUI Version: Checking for Update....'
  checkUpdateMenuItem.enabled = false
  const data = await (await fetch('https://api.github.com/repos/EasyAbp/AbpHelper.GUI/releases/latest', {type: 'text'})).json()
  var tagName = data.tag_name.replace('v', '')
  if (tagName) {
    checkUpdateMenuItem.label = tagName == currentVersion ? 'GUI Version: v' + currentVersion : 'GUI Version: v' + currentVersion + ' (Latest: v' + tagName + ')'
    checkUpdateMenuItem.enabled = true
    if (currentVersion != tagName) {
      downloadReleaseMenuItem.visible = true
    }
  } else if (data.message && data.message.indexOf('API rate limit exceeded') == 0) {
    checkUpdateMenuItem.label = 'GUI Version: Update checking failed (API rate limit exceeded)'
    checkUpdateMenuItem.enabled = true
  } else {
    checkUpdateMenuItem.label = 'GUI Version: Update checking failed'
    checkUpdateMenuItem.enabled = true
  }
  buildTrayMenuFromTemplate()
}

async function cliCheckForUpdate() {
  const currentVersion = getAbphelperCliVersion()
  cliUpdateMenuItem.visible = false
  cliCheckUpdateMenuItem.label = 'CLI Version: Checking for CLI Update....'
  cliCheckUpdateMenuItem.enabled = false
  const data = await (await fetch('https://api.github.com/repos/EasyAbp/AbpHelper.CLI/releases/latest', {type: 'text'})).json()
  if (data.tag_name) {
    cliCheckUpdateMenuItem.label = data.tag_name == currentVersion ? 'CLI Version: v' + currentVersion : 'CLI Version: v' + currentVersion + ' (Latest: v' + data.tag_name + ')'
    cliCheckUpdateMenuItem.enabled = true
    if (currentVersion != data.tag_name) {
      cliUpdateMenuItem.visible = true
    }
  } else if (data.message && data.message.indexOf('API rate limit exceeded') == 0) {
    cliCheckUpdateMenuItem.label = 'CLI Version: Update checking failed (API rate limit exceeded)'
    cliCheckUpdateMenuItem.enabled = true
  } else {
    cliCheckUpdateMenuItem.label = 'CLI Version: Update checking failed'
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

  if (!app.requestSingleInstanceLock()) {
    app.quit()
  }

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
  let cliCommand = process.platform === 'win32' ? '%USERPROFILE%\\.dotnet\\tools\\abphelper' : '$HOME/.dotnet/tools/abphelper'
  if (process.platform === 'win32') cliCommand = '@chcp 65001 >nul & cmd /d/s/c ' + cliCommand
  workerProcess = exec(cliCommand + ' --version', {cwd: '/'}, (error, stdout, stderr) => {
    if (error) {
      abphelperCliVersion = null
      console.log(error)
      console.log(stderr)
      return
    }
    abphelperCliVersion = stdout.replace(/[\r\n]/g, "")
    cliCheckForUpdate()
  })
}

initialize()
