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
    
    let foot = document.getElementsByTagName('footer')[0];
    if (foot.classList.contains(oldTheme))
      foot.classList.remove(oldTheme);
    foot.classList.add(newTheme);
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
  /*-- unthemed --*/
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
