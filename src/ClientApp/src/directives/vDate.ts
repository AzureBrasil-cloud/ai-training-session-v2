import { type DirectiveBinding } from 'vue'

/**
 * Helper function to format a date as 'dd/MM/yyyy - HH:mm'.
 * @param {HTMLElement} el - The element to update.
 * @param {Date|string|number} value - The date value to format.
 */
export default function formatDate(el: HTMLElement, bind: DirectiveBinding) {
  const raw = bind.value

  // Converte string ou timestamp em objeto Date
  const date = raw instanceof Date ? raw : new Date(raw)

  if (isNaN(date.getTime())) {
    console.warn('v-date directive expects a valid date.')
    return
  }

  const day = date.getDate().toString().padStart(2, '0')
  const month = (date.getMonth() + 1).toString().padStart(2, '0')
  const year = date.getFullYear()

  const hours = date.getHours().toString().padStart(2, '0')
  const minutes = date.getMinutes().toString().padStart(2, '0')

  const formatted = `${day}/${month}/${year} - ${hours}h${minutes}`
  el.innerText = formatted
}
