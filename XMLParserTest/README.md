# XMLParserTest

<p>
  Тестовая программа для проверки работоспособности библиотеки.
</p>
<p>
  Реализует следующие возможности:
  <ul>
    <li>загрузка XML из файла;</li>
    <li>редактирование текста XML в соответствующем поле;</li>
    <li>сохранение XML в файл;</li>
    <li>построение дерева XML по загруженному тексту (если корректен);</li>
    <li>выполнение манипуляций над деревом:
      <ul>
        <li>перемещение всех сыновей, содержащих только текст, в атрибуты;</li>
        <li>добавление элементам, содержащих сыновей, нового сына: <code>newChild</code> с атрибутом <code>status="added"</code>;</li>
      </ul>
  </ul>
</p>
