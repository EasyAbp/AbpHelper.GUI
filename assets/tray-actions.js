const { app } = require('electron').remote;

app.on('tray-nav-selected', (tag) => {
    let button = document.getElementById('button-' + tag)
    if (!button) return
    button.click()
});