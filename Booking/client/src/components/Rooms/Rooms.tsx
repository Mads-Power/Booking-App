import { useRooms } from "@api/getRooms";
import { Box } from "@mui/system";
import {
  CircularProgress,
  Button,
  Container,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  SelectChangeEvent,
} from "@mui/material";
import { Link } from "react-router-dom";
import { Seat } from "@type/seat";
import { WeekViewDatePicker } from "@components/WeekViewDatePicker";
import "./Rooms.module.css";
import { useEffect, useState } from "react";
import { useBookingsByRoom } from "@api/getBookingsByRoom";
import {
  DateContextType,
  useDateContext,
} from "@components/Provider/DateContextProvider";
import { useUserContext } from "@components/Provider/UserContextProvider";

export const Rooms = () => {
  const { isLoading, data, error } = useRooms();
  const { selectedDate: date, setSelectedDate: setDate }: DateContextType =
    useDateContext(); //'2023-01-02T13:42:16.115Z'
  const [room, setRoom] = useState(1);
  const occupiedSeats = useBookingsByRoom(room, date);
  const loggedInUser = useUserContext().user;

  useEffect(() => {}, [date, room]);

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  const handleChangeRoom = (e: SelectChangeEvent) => {
    setRoom(e.target.value as unknown as number);
    console.log("ROOM ", e.target.value);
  };

  const renderIsOccupied = (seatId: number) => {
    const predicate =
      occupiedSeats.data?.find((o) => o.seatId == seatId) != undefined;
    return predicate;
  };

  const renderOccupiedByLoggedInUser = (seatId: number) => {
    const predicate =
      occupiedSeats.data?.find(
        (o) => o.seatId == seatId && o.userId == loggedInUser.id
      ) != undefined;
    return predicate;
  };

  return (
    <>
      <Container>
        <WeekViewDatePicker />
      </Container>
      <Container>
        <FormControl fullWidth>
          <InputLabel>Room</InputLabel>
          <Select
            value={room as unknown as string}
            label="Room"
            onChange={handleChangeRoom}
          >
            {data?.map((rooms) => (
              <MenuItem key={rooms.id} value={rooms.id}>{rooms.name}</MenuItem>
            ))}
          </Select>
        </FormControl>
      </Container>
      <Container>
      <div className="flex flex-row">
        <div className="grow">
              Opptatt
        </div>
        <div className="">
              Booket av deg
        </div>
        
      </div>
      </Container>

      {data?.map((rooms) => (
        <div key={rooms.id}>
          {rooms.id == room ? (
            <div>
              <Container>
                <h1>{rooms.name}</h1>
              </Container>
              <Container
                sx={{
                  display: "flex",
                  flexWrap: "wrap",
                  backgroundColor: "#CECECE",
                  borderRadius: "25px",
                  flexDirection: "column-reverse",
                  p: 1,
                  border: "1px solid",
                }}
              >
                <Box
                  sx={{
                    display: "flex",
                    flexWrap: "wrap",
                    m: 2,
                    gap: 2,
                  }}
                >
                  {rooms.seats.map((seat: Seat) => (
                    <Link to={`/seat/${seat.id}`} key={seat.id} relative="path">
                      <Button
                        variant="contained"
                        sx={
                          renderOccupiedByLoggedInUser(seat.id)
                            ? { backgroundColor: "#61C577" }
                            : renderIsOccupied(seat.id)
                            ? { backgroundColor: "#DF8B0D" }
                            : { backgroundColor: "#54A4D1" }
                        }
                      >
                        {seat.id}
                      </Button>
                    </Link>
                  ))}
                </Box>
              </Container>
            </div>
          ) : (
            <div></div>
          )}
        </div>
      ))}
    </>
  );
};
