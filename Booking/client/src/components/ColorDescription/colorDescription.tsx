import { Box, Container } from "@mui/material"

export const ColorDescription = () => {
    return <>
    <div className="w-[90%] mx-auto">
        <div className="flex flex-row w-full">
          <Box className="grow flex flex-row border w-1/2 h-8">
            <div className="py-1">
              <svg xmlns="http://www.w3.org/2000/svg" className="fill-[#3981F1]" height="24" width="24"><path d="M6 18V6h12v12Z" /></svg>
            </div>
            <Container className="text-center">
              <span className="align-middle text-sm truncate">
                Opptatt
              </span>
            </Container>
          </Box>
          <Box className="grow flex flex-row border w-1/2 h-8">
            <div className="py-1">
              <svg xmlns="http://www.w3.org/2000/svg" className="fill-[#68B984]" height="24" width="24"><path d="M6 18V6h12v12Z" /></svg>
            </div>
            <Container className="text-center">
              <span className="align-middle text-sm truncate">
                Booket av deg
              </span>
            </Container>
          </Box>
        </div>
      </div>
    </>
}