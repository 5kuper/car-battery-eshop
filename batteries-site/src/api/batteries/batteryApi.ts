import httpClient from './httpClient'
import type { Battery, BatteryForm } from './contracts/batteryApiContracts'

export const getBatteries = async (): Promise<Battery[]> => {
  const response = await httpClient.get<Battery[]>('/api/Batteries')
  return response.data
}

export const getBatteryById = async (id: string): Promise<Battery> => {
  const response = await httpClient.get<Battery>(`/api/Batteries/${id}`)
  return response.data
}

export const createBattery = async (data: BatteryForm): Promise<{ id: string }> => {
  const response = await httpClient.post<{ id: string }>('/api/Batteries', null, { params: data })
  return response.data
}

export const updateBattery = async (id: string, data: Partial<BatteryForm>): Promise<void> => {
  await httpClient.patch(`/api/Batteries/${id}`, null, { params: { ...data } })
}

export const deleteBattery = async (id: string): Promise<void> => {
  await httpClient.delete(`/api/Batteries/${id}`)
}
