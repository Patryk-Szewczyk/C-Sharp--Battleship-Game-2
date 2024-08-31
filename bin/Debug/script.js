const credits = {
    sld: document.querySelector('div.vertical-slider'),
    sldFun() {
        credits.sld.style.top = ((credits.sld.getBoundingClientRect().height/* + window.innerHeight*/) * -1) + "px";
        // Nie muszę podawać window.innerHeight, gdyż zmieniam wartość właściwości .top, która wcześniej była ustawiona na 100% w kontekście pozycji
        // "absolute",więc znika mi ów 100% z top i różnica względem bazowego położenia znika i z tego właśnie powodu nie trzeba brać tam window.innerHeight.
        credits.sld.style.transitionDuration = "160s";  // 200s
        credits.sld.style.transitionTimingFunction = "linear";
    },
};
credits.sldFun();