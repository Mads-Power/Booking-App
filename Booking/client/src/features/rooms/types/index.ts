import { Seat } from '../../seats/types';
import { OfficeType } from '../../../types';

export type Room = {
  id: number;
  name: string;
  capacity: number;
  officeId: number;
  seats: Seat[];
};
