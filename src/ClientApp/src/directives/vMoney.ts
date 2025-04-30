import { type DirectiveBinding } from 'vue'

const formatter = new Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'USD',
})

/**
 * Helper function to format a number as currency.
 * @param {HTMLElement} el - The element to update.
 * @param {number|string} value - The value to format.
 */
export default function formatMoney(el: HTMLElement, bind: DirectiveBinding) {
  // Convert the value to a number (if it isn't already)
  const number = typeof bind.value === 'number' ? bind.value : parseFloat(bind.value)
  if (isNaN(number)) {
    console.warn('v-money directive expects a valid number.')
    return
  }
  // Format the number as US dollars. Change locale or currency if needed.
  const formatted = formatter.format(number)
  el.innerText = formatted
}
