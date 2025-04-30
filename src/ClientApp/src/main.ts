import './scss/main.scss'

// Import Bootstrap JavaScript features (optional)
import 'bootstrap'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import OffCanvas from './plugins/offcanvas'
import vMoney from './directives/vMoney'
import vDate from './directives/vDate'

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(OffCanvas)

app.directive('money', vMoney)
app.directive('date', vDate)

app.mount('#app')
