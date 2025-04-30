import { ref } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios'
import type { User } from '@/models/user'

const useUserStore = defineStore('user', () => {
  const user = ref<User | null>(null)

  const getUser = async () => {
    if (user.value != null) {
      return
    }

    try {
      const response = await axios.get<User>('/api/user')
      user.value = response.data
      return user
    } catch (error) {
      console.error('Error fetching user:', error)
      user.value = null
      return null
    }
  }

  return { user, getUser }
})

export { useUserStore }
