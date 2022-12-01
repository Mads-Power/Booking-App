import { Seat } from '../../seats/types';
import { OfficeType } from '../../../types';

export type Room = {
  id: number;
  name: string;
  capacity: number;
  office: OfficeType;
  seats: Seat;
};
