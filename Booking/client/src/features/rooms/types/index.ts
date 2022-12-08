import { Seats } from '../../seats/types';
import { OfficeType } from '../../../types';

export type Rooms = {
  id: number;
  name: string;
  capacity: number;
  officeId: number;
  seats: Seats[];
};
