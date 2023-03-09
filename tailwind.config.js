/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/components/*.{vue,js}", "./src/pages/*/*.{vue,js}", "./src/pages/*.{vue,js}", "./src/pages/*/*/*.{vue,js}", './node_modules/tw-elements/dist/js/**/*.js'
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('tw-elements/dist/plugin')
  ],
}
