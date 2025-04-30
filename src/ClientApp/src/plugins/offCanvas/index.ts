import { reactive, type App } from 'vue'
import Offcanvas from './OffcanvasPlugin.vue'

export interface IOffcanvas {
  active(): string
  show(name: string): void
  close(): void
}

const _current = reactive({
    name: '',
  }),
  api = {
    active() {
      return _current.name
    },
    show(name: string) {
      _current.name = name
    },
    close() {
      _current.name = ''
    },
  },
  plugin = {
    install(App: App) {
      // Register global component
      // eslint-disable-next-line vue/multi-word-component-names
      App.component('Offcanvas', Offcanvas)

      // Provide API
      App.provide('$offcanvas', api)
    },
  }

export default plugin
