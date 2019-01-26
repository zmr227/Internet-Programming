/*---- set class attribute on body ----------------------*/

function setBodyTheme(theme) {
  var test = document.getElementsByClassName(theme)[0];
  if (test) {
    var elem = document.getElementsByTagName('body')[0];
    if (elem.classList.contains(theme))
      return;
    elem.classList.add(theme);
  }
}
/*---- set class attribute on all links -----------------*/

function setLinkTheme(theme) {
  for (let i = 0, l = document.links.length; i < l; ++i) {
    let lnk = document.links[i];
    if (lnk.classList.contains(theme))
      return;
    lnk.classList.add(theme);
  }
}
/*---- change class attribute on body -------------------*/

function swapBodyTheme(oldTheme, newTheme) {
  var test = document.getElementsByClassName(oldTheme)[0];
  if (test) {
    let elem = document.getElementsByTagName('body')[0];
    if (elem.classList.contains(oldTheme))
      elem.classList.remove(oldTheme);
    elem.classList.add(newTheme);
    
  }
}
/*---- change class attributes on all links -------------*/

function swapLinkTheme(oldTheme, newTheme) {
  let test = document.getElementsByClassName(oldTheme)[0];
  if (!test) {
    return;
  }
  for (let i = 0, l = document.links.length; i < l; ++i) {
    let lnk = document.links[i];
    if (lnk.classList.contains(oldTheme))
      lnk.classList.remove(oldTheme);
    lnk.classList.add(newTheme);
  }
}
/*---- set theme on page --------------------------------*/

function setTheme() {
  var lightBox = JSON.parse(localStorage.getItem('checkbox1'));
  var darkBox = JSON.parse(localStorage.getItem('checkbox2'));
  var pageBox = JSON.parse(localStorage.getItem('checkbox3'));

  document.getElementById('light-theme').checked = lightBox;
  document.getElementById('dark-theme').checked = darkBox;
  document.getElementById('page-theme').checked = pageBox;

  var curTheme = localStorage.getItem("cur-theme");
  let elem = document.getElementsByTagName('body')[0];

  if (curTheme == 'lighttheme') {
    if (elem.classList.contains('darktheme')) {
      swapBodyTheme('darktheme', 'lighttheme');
      swapLinkTheme('linkdarktheme', 'linklighttheme');
    }
    else {
      elem.classList.add('lighttheme');
    }
    return;
  }
  else if (curTheme == 'darktheme') {
    if (elem.classList.contains('lighttheme')) {
      swapBodyTheme('lighttheme', 'darktheme');
      swapLinkTheme('linklighttheme', 'linkdarktheme');
    }
    else {
      elem.classList.add('darktheme');
    }
    return;
  }
  else {
    document.getElementById('page-theme').checked = true;
    let testdark = document.getElementsByClassName('darktheme')[0];
    if (testdark) {
      setBodyTheme('darktheme');
      setLinkTheme('linkdarktheme');
      return;
    }
    let testlight = document.getElementsByClassName('lighttheme')[0];
    if (testlight) {
      setBodyTheme('lighttheme');
      setLinkTheme('linklighttheme');
      return;
  }
  }
  
  /*-- unthemed --*/
}

function checkBox(obj){
  if(obj.id == 'light-theme' && obj.checked){
  document.getElementById('dark-theme').checked = false;
  document.getElementById('page-theme').checked = false;
  }
  else if(obj.id == 'dark-theme' && obj.checked){
  document.getElementById('light-theme').checked = false;
  document.getElementById('page-theme').checked = false;
  }
  else if(obj.id == 'page-theme' && obj.checked){
  document.getElementById('light-theme').checked = false;
  document.getElementById('dark-theme').checked = false;
  }
  saveTheme();
  }

function checkTheme(obj) {
  checkBox(obj);
  setTheme();
}

/*---- change theme on page -----------------------------*/

function toggleTheme() {
  var elem = document.getElementsByTagName('body')[0];
  if (elem.classList.contains('lighttheme')) {
    swapBodyTheme('lighttheme', 'darktheme');
    swapLinkTheme('linklighttheme', 'linkdarktheme');
  }
  else {
    if (elem.classList.contains('darktheme')) {
      swapBodyTheme('darktheme', 'lighttheme');
      swapLinkTheme('linkdarktheme', 'linklighttheme');
    }
  }
}

/*---- save theme on page --------------------------------*/
function saveTheme() {	
  var lighten = document.getElementById('light-theme').checked;
  var darken = document.getElementById('dark-theme').checked;
  var page = document.getElementById('page-theme').checked;

  localStorage.setItem("checkbox1", lighten);	
  localStorage.setItem("checkbox2", darken);
  localStorage.setItem("checkbox3", page);

  if (lighten) {
    localStorage.setItem("cur-theme", 'lighttheme');
  }
  else if (darken) {
    localStorage.setItem("cur-theme", 'darktheme');
  }
  else {
    localStorage.setItem("cur-theme", 'pagetheme');
  }

}
