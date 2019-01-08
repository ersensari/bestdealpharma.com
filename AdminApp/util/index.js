
var CryptoJS = require('crypto-js')

const randomElement = (arr = []) => {
  return arr[Math.floor(Math.random() * arr.length)]
}

const kebab = (str) => {
  return (str || '').replace(/([a-z])([A-Z])/g, '$1-$2').toLowerCase()
}

const toggleFullScreen = () => {
  let doc = window.document
  let docEl = doc.documentElement

  let requestFullScreen = docEl.requestFullscreen || docEl.mozRequestFullScreen || docEl.webkitRequestFullScreen || docEl.msRequestFullscreen
  let cancelFullScreen = doc.exitFullscreen || doc.mozCancelFullScreen || doc.webkitExitFullscreen || doc.msExitFullscreen

  if (!doc.fullscreenElement && !doc.mozFullScreenElement && !doc.webkitFullscreenElement && !doc.msFullscreenElement) {
    requestFullScreen.call(docEl)
  } else {
    cancelFullScreen.call(doc)
  }
}

const secretKey = '60EFA649C72C4118A989E3CE6D795579'
const encrypt = (obj) => {
  var ciphertext = CryptoJS.AES.encrypt(JSON.stringify(obj), secretKey)
  return ciphertext.toString()
}

const decrypt = (ciphertext) => {
  var bytes = CryptoJS.AES.decrypt(ciphertext.toString(), secretKey)
  var decryptedData = JSON.parse(bytes.toString(CryptoJS.enc.Utf8))
  return decryptedData
}

export default {
  randomElement,
  toggleFullScreen,
  kebab,
  encrypt,
  decrypt
}
