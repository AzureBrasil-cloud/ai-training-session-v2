import type { MessageResult } from '@/models/messageResult'
import type { Thread } from '@/models/thread'
import axios, { AxiosError } from 'axios'

const service = {
  get: async (id: string) => {
    try {
      const response = await axios.get<Thread>(`/api/threads/${id}`)
      return response.data
    } catch (error: unknown) {
      const axiosError = error as AxiosError
      throw axiosError?.response?.data
    }
  },

  getMessages: async (id: string) => {
    try {
      const response = await axios.get<MessageResult[]>(`/api/threads/${id}/messages`)
      return response.data
    } catch (error: unknown) {
      const axiosError = error as AxiosError
      throw axiosError?.response?.data
    }
  },

  create: async (thread: Thread) => {
    try {
      const response = await axios.post<Thread>('/api/threads', thread)
      return response.data
    } catch (error: unknown) {
      const axiosError = error as AxiosError
      throw axiosError?.response?.data
    }
  },
}

export default service
