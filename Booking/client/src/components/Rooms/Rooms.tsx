import { useRoomsQuery } from '@api/useRoomsQuery';
import { CircularProgress, Button, Container, SelectChangeEvent, FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import { WeekViewDatePicker } from '@components/WeekViewDatePicker';
import { ColorDescription } from '@components/ColorDescription/colorDescription'
import './Rooms.module.css';
import { useEffect, useState } from 'react';
import { useBookingsByRoomQuery } from '@api/useBookingsByRoomQuery';
import { useAtom } from 'jotai';
import { dateAtom } from '../Provider/jotaiProvider';
import { useUserQuery } from '@api/useUserQuery';
import { Room } from "@type/room";
import { useNavigate } from 'react-router-dom';
import { DeskContainer } from './DeskContainer';

export const Rooms = () => {
  const { isLoading, data, error } = useRoomsQuery();
  const [date, setDate] = useAtom(dateAtom);
  const { data: loggedInUser } = useUserQuery('5');
  const [room, setRoom] = useState(1);
  const occupiedSeats = useBookingsByRoomQuery(room, date);
  const [selectedRoom, setSelectedRoom] = useState<Room>()

  useEffect(() => {
    if (data) {
      if (!room) setRoom(1);
      setSelectedRoom(data[room - 1]);
    }

  }, [date, room, data, occupiedSeats]);

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  const handleChangeRoom = (e: SelectChangeEvent) => {
    const val = parseInt(e.target.value);
    if (data) {
      setRoom(val);
      setSelectedRoom(data[val - 1]);
    }
  };

  const handleSelectRoomFromChild = (roomId: number) => {
    if (data) {
      setRoom(roomId);
      setSelectedRoom(data[roomId + 1]);
    }
  }

  return (
    <div className="h-full w-[85%] overflow-hidden flex flex-col gap-4 my-10 mx-auto">
      <Container className='overflow-hidden'>
        <WeekViewDatePicker />
      </Container>
      {/* This div will be hidden on screens smaller than 1024 px */}
      {/* Roompicker for smaller screens */}
      <div className="w-full mt-12 mb-8 lg:hidden">
        <FormControl fullWidth>
          <InputLabel>Rom</InputLabel>
          <Select
            value={room.toString()}
            label="Room"
            onChange={handleChangeRoom}
          >
            {data?.map((rooms) => (
              <MenuItem key={rooms.id} value={rooms.id}>{rooms.name}</MenuItem>
            ))}
          </Select>
        </FormControl>
      </div>

      {/* Color description */}
      <ColorDescription />

      {loggedInUser && data && selectedRoom ? (
        <DeskContainer
          data={data}
          selectedRoomFromParent={selectedRoom}
          user={loggedInUser}
          occupiedSeats={occupiedSeats}
          setSelectedRoom={handleSelectRoomFromChild}
        />
      ) : (
        <></>
      )}
    </div>
  );
}
