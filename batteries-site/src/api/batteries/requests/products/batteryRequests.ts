import httpClient from '../../httpClient'
import type { Response } from '../../response'
import type { Battery, BatteryForm } from '../../models/batteryModels'

export const batteryRequests = {
  getBatteries: async (): Promise<Response<Battery[]>> => {
    const result = await httpClient.get<Response<Battery[]>>('/api/Batteries')
    return result.data
  },
  getBatteryById: async (id: string): Promise<Response<Battery>> => {
    const result = await httpClient.get<Response<Battery>>(`/api/Batteries/${id}`)
    return result.data
  },
  createBattery: async (data: BatteryForm): Promise<Response<{ id: string }>> => {
    const result = await httpClient.post<Response<{ id: string }>>('/api/Batteries', data)
    return result.data
  },
  updateBattery: async (id: string, data: Partial<BatteryForm>): Promise<Response<void>> => {
    const result = await httpClient.patch<Response<void>>(`/api/Batteries/${id}`, data)
    return result.data
  },
  deleteBattery: async (id: string): Promise<Response<void>> => {
    const result = await httpClient.delete(`/api/Batteries/${id}`)
    return result.data
  },
}
