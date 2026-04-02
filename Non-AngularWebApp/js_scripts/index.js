//imports of modules and components go here

let currentTheme = "dark";


const toggleThemeButton = document.getElementById("darkModeToggle");

toggleThemeButton.addEventListener("click", () => toggleTheme());

Init();

function Init() {
    console.log("Hello World!");
    console.log(toggleThemeButton);
}

function toggleTheme() {
    console.log("Button Clicked!");

    if (currentTheme === "dark")
        darkTheme();
    else if (currentTheme === "light")
        lightTheme();

    //fix this up to use local cache and to use the proper colors. Maybe use another variable in css or something, not sure.
    //store variable in local cache storage to tell what the current theme is on load
    //
    //also code up the way you would do it if you had to get them by elementid and change the color directly.
}

function darkTheme() {
    document.documentElement.style.setProperty("--primary-page-color", "white");
    document.documentElement.style.setProperty("--secondary-page-color", "lightgray");
    document.documentElement.style.setProperty("--primary-font-color", "black");
    currentTheme = "light";
}

function lightTheme() {
    document.documentElement.style.setProperty("--primary-page-color", "black");
    document.documentElement.style.setProperty("--secondary-page-color", "darkgray");
    document.documentElement.style.setProperty("--primary-font-color", "white");
    currentTheme = "dark";
}