const DATA_THEME_ATTRIBUTE = 'data-current-theme';
const LIGHT_THEME = 'light-theme';
const DARK_THEME = 'dark-theme';
const BUTTON_CLASS = 'btn';

const themeBtn = document.querySelector(`.${BUTTON_CLASS}`);
if (!themeBtn) throw new Error('No theme button found');

themeBtn.addEventListener('click', () => {
  const currentTheme = themeBtn.getAttribute(DATA_THEME_ATTRIBUTE);
  if (currentTheme === LIGHT_THEME) {
    document.body.classList = DARK_THEME;
    themeBtn.setAttribute(DATA_THEME_ATTRIBUTE, DARK_THEME);
    themeBtn.textContent = 'Dark';
  } else {
    document.body.classList = 'light-theme';
    themeBtn.setAttribute(DATA_THEME_ATTRIBUTE, LIGHT_THEME);
    themeBtn.textContent = 'Light';
  }
});
