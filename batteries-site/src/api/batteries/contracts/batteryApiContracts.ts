export enum StartPowerRating {
  SAE,
  EN,
  IEC,
  DIN,
}

export interface Battery {
  id: string
  created: string
  updated: string
  name: string
  capacity: number
  voltage: number
  startPower: number
  startPowerRating: StartPowerRating
  price: number
  imageUrl?: string
}

export interface BatteryForm {
  name: string
  capacity: number
  voltage: number
  startPower: number
  startPowerRating: StartPowerRating
  price: number
}

export function mapBatteryToForm(battery: Battery): BatteryForm {
  return {
    name: battery.name,
    capacity: battery.capacity,
    voltage: battery.voltage,
    startPower: battery.startPower,
    startPowerRating: battery.startPowerRating,
    price: battery.price,
  }
}
