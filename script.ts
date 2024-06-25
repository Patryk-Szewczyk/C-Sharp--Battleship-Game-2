console.log("hej!");

const verticalSlider_FUN: {
    vertSld: HTMLDivElement,
    slideUp_MTD: Function
} = {
    vertSld: document.querySelector('div.vertical-slider'),
    slideUp_MTD() {
        verticalSlider_FUN.vertSld.style.top = ((verticalSlider_FUN.vertSld.getBoundingClientRect().height/* + window.innerHeight*/) * -1) + "px";
        // Nie muszę podawać window.innerHeight, gdyż zmieniam wartość właściwości .top, która wcześniej była ustawiona na 100% w kontekście pozycji
        // "absolute",więc znika mi ów 100% z top i różnica względem bazowego położenia znika i z tego właśnie powodu nie trzeba brać tam window.innerHeight.
        verticalSlider_FUN.vertSld.style.transitionDuration = "160s";  // 200s
        verticalSlider_FUN.vertSld.style.transitionTimingFunction = "linear";
    },
};
verticalSlider_FUN.slideUp_MTD();