import httpClient from './httpClient'
import type { Battery } from './contracts/batteryApiContracts'

export const getBatteries = async (): Promise<Battery[]> => {
  const response = await httpClient.get<Battery[]>('/api/Batteries')
  return response.data
}

export const getBatteryById = async (id: string): Promise<Battery> => {
  const response = await httpClient.get<Battery>(`/api/Batteries/${id}`)
  return response.data
}

export const createBattery = async (data: Partial<Battery>): Promise<Battery> => {
  const response = await httpClient.post<Battery>('/api/Batteries', null, { params: data })
  return response.data
}

export const updateBattery = async (id: string, data: Partial<Battery>): Promise<void> => {
  await httpClient.put(`/api/Batteries/${id}`, null, { params: { ...data, id } })
}

export const deleteBattery = async (id: string): Promise<void> => {
  await httpClient.delete(`/api/Batteries/${id}`)
}
