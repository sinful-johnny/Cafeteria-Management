import { FaChevronDown, FaChevronUp } from 'react-icons/fa';
import "./CollaspsibleListPenis.css";

const spawnListComponent = (content) => {
    return content.map(shape => {
      const handleDragStart = (event, data) => {
        event.dataTransfer.setData('text/plain', JSON.stringify(data));
      };

      return (
        <div className="collaspsible-listItem"
          draggable
          onDragStart={(event) => handleDragStart(event, shape)}
          key={shape.shapeId !== undefined ? shape.shapeId : shape.ID_FOOD}
        >
          <p>{shape.amount} x </p>
          <img src={shape.img}/>
          <span>{shape.name}</span>
        </div>
      );
    });
  }

  const CollapsibleListPenis = ({items, openIndexes, setOpenIndexes}) => {
    const toggleItem = (index) => {
        setOpenIndexes((prev) => ({
          ...prev,
          [index]: !prev[index]
        }));
      };
  
    return (
      items.map((item, index) => ( item.content && item.content.length > 0 &&
        <div key={item.id}>
          <div
            onClick={() => toggleItem(index)}
            className="collapsible-header"
          >
            <span>
              {openIndexes[index] ? <FaChevronUp /> : <FaChevronDown />}
            </span>
            <span>{item.title}</span>
          </div>
          {openIndexes[index] && (
            <div>
              {spawnListComponent(item.content, item.amount)}
            </div>
          )}
        </div>
      ))
    )
  }

export default CollapsibleListPenis;