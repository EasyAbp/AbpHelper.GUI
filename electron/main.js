// require('update-electron-app')({
//   logger: require('electron-log')
// })

const fetch = require('electron-main-fetch')
const path = require('path')
const {spawn, exec} = require('child_process')
const {app, Menu, Tray, BrowserWindow, shell, dialog} = require('electron')

const debug = /--dev-debug/.test(process.argv[2])

if (process.mas) app.setName('AbpHelper')

if (process.platform === 'darwin') require('fix-path')()

let mainWindow = null
let contextMenu = null
let blazorHost = null

let forceQuit = false

function initialize () {
  makeSingleInstance()

  function runBlazorHost() {
    if (process.platform === 'darwin'){
      let workfolder = path.join(__dirname.replace('/Resources/app.asar', ''), "/dotnet/EasyAbp.AbpHelper.Gui.Blazor")
      blazorHost = spawn('dotnet', ['EasyAbp.AbpHelper.Gui.Blazor.dll', '--urls', 'https://localhost:44313'],{cwd: workfolder})
    }else{
      blazorHost = spawn('dotnet', ['EasyAbp.AbpHelper.Gui.Blazor.dll', '--urls', 'https://localhost:44313'], {cwd: "./dotnet/EasyAbp.AbpHelper.Gui.Blazor"})
    }
    
    blazorHost.on('close', function (code) {
      if (code !== 0) {
        console.log(`grep process exited with code ${code}`);
      }
      forceQuit = true;
      app.quit()
    })
  }

  process.on('exit', function () {
    if (blazorHost != null) blazorHost.kill(2)
  });

  function createTray() {
    let trayIcon = process.platform === 'darwin' ? '/assets/app-icon/tray/icon-darwin.png' : '/assets/app-icon/tray/icon.png'
    console.log(trayIcon)
    tray = new Tray(path.join(__dirname, trayIcon))
    buildTrayMenuFromTemplate()

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
        nodeIntegration: true,
        webSecurity: false
      },
      icon: path.join(__dirname, '/assets/app-icon/png/32.png')
    }

    if (process.platform === 'linux') {
      windowOptions.icon = path.join(__dirname, '/assets/app-icon/png/512.png')
    }

    mainWindow = new BrowserWindow(windowOptions)
    mainWindow.setMenuBarVisibility(false)
    mainWindow.loadURL(`file://${__dirname}/index.html`)

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
    mainWindow.webContents.setWindowOpenHandler(({ url }) => {
      shell.openExternal(url);
    });
  }

  app.on('ready', () => {
    let checkDotnetProcess = exec("dotnet --version", (error, stdout, stderr) => {
      if (error) {
        dialog.showMessageBoxSync({type: "error", message: "Required .NET 5.0+ runtime is not installed."})
        app.quit()
        return
      }
      
      let trustCertProcess = exec("dotnet dev-certs https --trust")
    
      trustCertProcess.on('close', function (code) {
        runBlazorHost()
        setTimeout(function() {
          createTray()
          createWindow()
        }, 1000);
      })
    });
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

let template = [{
  label: 'Show',
  click: () => mainWindow.show()
//   label: 'Abp CLI...',
//   click: () => loadShowPage('abp-cli-new')
// }, {
//   label: 'AbpHelper CLI...',
//   click: () => loadShowPage('abphelper-cli-generate-crud')
// }, {
//   label: 'Modules Manager...',
//   click: () => loadShowPage('modules-manager-market')
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
    }, {
      label: 'Discord channel',
      icon: path.join(__dirname, '/assets/app-icon/menuitem/discord/icon.png'),
      click: () => shell.openExternal('https://discord.gg/S6QaezrCRq')
    }]
  },
  {
    label: 'About...',
    // click: () => loadShowPage('about')
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

initialize()