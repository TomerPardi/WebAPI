import React, { useRef, useEffect } from "react";

function useAlerter(ref, props) {
  useEffect(() => {
    // if click on backgroud, nullify current user
    function handleBgClick(event) {
      let modal = document.getElementsByClassName("modal-dialog");
      // ugly patch to check if there is a modal in the foregfround
      if (modal.length > 0 && modal[0].hidden == false) return;
      if (ref.current && !ref.current.contains(event.target)) {
        props.setter("none");
      }
    }
    // add listener
    window.addEventListener("mousedown", handleBgClick);
    return () => {
      // remove listener during cleaning stage
      window.removeEventListener("mousedown", handleBgClick);
    };
  }, [ref]);
}

export default function OutsideAlerter(props) {
  const wrapperRef = useRef(null);
  useAlerter(wrapperRef, props);
  return (
    <div ref={wrapperRef} className='wrap'>
      {props.children}
    </div>
  );
}
